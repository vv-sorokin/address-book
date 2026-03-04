import { Injectable, computed, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AuthApi } from '../core/api/auth.api';
import { AuthService } from '../core/auth/auth.service';

type UserInfo = { id: string; email: string; role: string };

@Injectable({ providedIn: 'root' })
export class AuthStore {
  private _loading = signal(false);
  loading = this._loading.asReadonly();

  private _user = signal<UserInfo | null>(null);
  user = this._user.asReadonly();


  isAuthenticated = computed(() => !!this._user());

  constructor(private api: AuthApi, private auth: AuthService) {}

  async login(email: string, password: string) {
    this._loading.set(true);
    try {
      const res = await firstValueFrom(this.api.login(email, password));
      this.auth.setToken(res.accessToken);
      this._user.set(res.user);
    } finally {
      this._loading.set(false);
    }
  }


  logout() {
    this.auth.clearToken();
    this._user.set(null);
  }
}