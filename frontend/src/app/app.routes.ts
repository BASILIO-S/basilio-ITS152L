import { Routes } from '@angular/router';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { ListsPostsComponent } from './components/lists-posts/lists-posts.component';
import { PostDetailComponent } from './components/post-detail/post-detail.component';

export const routes: Routes = [
  { path: '', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'posts', component: ListsPostsComponent },
  { path: 'posts/:id', component: PostDetailComponent },
];
