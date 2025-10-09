import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TokenStorageService } from '../../services/token-storage.service';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  form: any = {
    userName: '',
    password: ''
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private tokenStorage: TokenStorageService
  ) {}

  ngOnInit(): void {}

  onSubmit(): void {
    const payload = {
      userName: this.form.userName,
      password: this.form.password
    };

    this.http.post('http://localhost:5208/api/Login/login', payload, { responseType: 'text' }).subscribe({
    next: (data) => {
      console.log('Login success:', data);
      this.tokenStorage.saveToken(data);
    
      alert('Login successful!');
      this.router.navigate(['/posts']);
    },
    error: (err) => {
      console.error('Login failed:', err);
      alert('Invalid username or password.');
    }});
  }

  goToRegister(): void {
    this.router.navigate(['/register']);
  }
}

