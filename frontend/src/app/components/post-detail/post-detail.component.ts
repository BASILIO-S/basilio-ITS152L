import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

interface Post {
  id: number;
  title: string;
  body: string;
  userName: string;
  dateCreated: string;
  firstName: string;
  lastName: string;
}

@Component({
  selector: 'app-post-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.css']
})
export class PostDetailComponent implements OnInit, OnDestroy {
  private routeSub!: Subscription;
  id: number = 0;
  post?: Post;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    // ✅ Get the ID from the route parameter
    this.routeSub = this.route.params.subscribe(params => {
      this.id = +params['id']; // "+" converts it to number
      this.initData();
    });
  }

  initData(): void {
    // ✅ Fixed HttpClient call syntax
    this.http.get<Post>(`http://localhost:5208/api/Post/${this.id}`).subscribe({
      next: (data: Post) => {
        this.post = data;
        console.log('Loaded post:', this.post);
      },
      error: (err) => {
        console.error('Error loading post:', err);
      }
    });
  }

  ngOnDestroy(): void {
    // ✅ Clean up the route subscription
    this.routeSub.unsubscribe();
  }
}
