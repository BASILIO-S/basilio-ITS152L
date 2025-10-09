import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-lists-posts',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lists-posts.component.html',
  styleUrls: ['./lists-posts.component.css']
})
export class ListsPostsComponent {
  posts: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get('http://localhost:5208/api/Post/list').subscribe({
      next: data => (this.posts = data as any[]),
      error: err => console.error('Error loading posts:', err)
    });
 }
}
