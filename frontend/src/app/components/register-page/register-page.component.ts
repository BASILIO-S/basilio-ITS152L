import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  form: any = {
    userName: null,   // ✅ Added this
    password: null,
    firstName: null,
    lastName: null
  };

  constructor(public router: Router, private http: HttpClient) {}

  ngOnInit(): void {}

  onSubmit(): void {
    console.log('Form data:', this.form);

    this.http.post(
      'http://localhost:5208/api/Login/register',
      this.form,
      { responseType: 'text' }
    ).subscribe({
      next: () => {
        alert('Registration successful!');
        this.router.navigate(['/']); // Go back to login
      },
      error: (err) => {
        console.error('Registration failed:', err);
        alert('Failed to register. Please try again.');
      }
    });
  }

  goToLogin(): void {
    this.router.navigate(['/']);
  }
}
