using Microsoft.EntityFrameworkCore;
using TechritizeAuthAPI.Data;
using TechritizeAuthAPI.Models;

namespace TechritizeAuthAPI.Services
{
    public class TwoFactorService
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public TwoFactorService(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // Generate and send 6-digit code
        public async Task<string> GenerateAndSendCodeAsync(string email)
        {
            email = email?.Trim();
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            var code = new Random().Next(100000, 999999).ToString();

            // Remove old codes
            var oldCodes = _context.TwoFactorCodes.Where(c => c.Email == email);
            _context.TwoFactorCodes.RemoveRange(oldCodes);

            var twoFactorCode = new TwoFactorCode
            {
                Email = email,
                Code = code,
                ExpiryTime = DateTime.UtcNow.AddMinutes(1)
            };

            await _context.TwoFactorCodes.AddAsync(twoFactorCode);
            await _context.SaveChangesAsync();

            try
            {
                // Send code via email
                await _emailService.SendEmailAsync(email, "Your Verification Code",
                    $"<h2>Your verification code is: <b>{code}</b></h2><p>It will expire in 1 minute.</p>");
            }
            catch (Exception ex)
            {
                // Log exception properly
                throw new InvalidOperationException($"Failed to send verification email: {ex.Message}", ex);
            }

            return code;
        }

        // Validate code
        public async Task<bool> ValidateCodeAsync(string email, string code)
        {
            email = email?.Trim();
            if (string.IsNullOrWhiteSpace(email)) return false;

            var twoFactorCode = await _context.TwoFactorCodes
                .Where(c => c.Email == email && c.Code == code)
                .FirstOrDefaultAsync();

            if (twoFactorCode == null) return false;
            if (DateTime.UtcNow > twoFactorCode.ExpiryTime) return false;

            _context.TwoFactorCodes.Remove(twoFactorCode);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
