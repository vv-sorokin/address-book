import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { env } from '../config/env';
import { AuthResponse } from './dtos';


@Injectable({ providedIn: 'root' })
export class AuthApi {
  private http = inject(HttpClient);

  login(email: string, password: string) {
    return this.http.post<AuthResponse>(`${env.apiBaseUrl}/auth/login`, { email, password });
  }

  register(email: string, password: string) {
    return this.http.post<AuthResponse>(`${env.apiBaseUrl}/auth/register`, { email, password });
  }
}