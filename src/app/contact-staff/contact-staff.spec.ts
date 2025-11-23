import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactStaff } from './contact-staff';

describe('ContactStaff', () => {
  let component: ContactStaff;
  let fixture: ComponentFixture<ContactStaff>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactStaff]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContactStaff);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
