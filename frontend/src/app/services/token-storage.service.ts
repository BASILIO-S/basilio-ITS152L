import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  private isBrowserEnv: boolean;

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    this.isBrowserEnv = isPlatformBrowser(this.platformId);
  }

  signOut(): void {
    if (this.isBrowserEnv) {
      sessionStorage.clear();
    }
  }

  public saveToken(token: string): void {
    if (this.isBrowserEnv) {
      sessionStorage.removeItem(TOKEN_KEY);
      sessionStorage.setItem(TOKEN_KEY, token);
    }
  }

  public getToken(): string | null {
    if (this.isBrowserEnv) {
      return sessionStorage.getItem(TOKEN_KEY);
    }
    return null;
  }

  public saveUser(id: number): void {
    if (this.isBrowserEnv) {
      sessionStorage.removeItem(USER_KEY);
      sessionStorage.setItem(USER_KEY, id.toString());
    }
  }
}
