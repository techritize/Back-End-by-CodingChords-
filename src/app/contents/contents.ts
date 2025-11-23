import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, User } from '../services/api.service';

export interface ProducedContent {
  id: number;
  title: string;
  description: string;
  keyPoints: string[];
  files: string[]; // URLs to images, videos, or audio
  timestamp: string;
}

@Component({
  selector: 'app-contents',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contents.html',
  styleUrls: ['./contents.css']
})
export class Contents implements OnInit {
  contents: ProducedContent[] = [];
  isLoading = true;
  message = '';

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadContents();
  }

loadContents() {
  this.api.getClientContents().subscribe({
    next: (res) => {
      this.contents = res as ProducedContent[];
      this.isLoading = false;
    },
    error: (err) => {
      console.error('Error loading contents:', err);
      this.message = '❌ Failed to load contents.';
      this.isLoading = false;
    }
  });
  }
}
