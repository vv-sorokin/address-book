import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { env } from '../config/env';
import { ContactDto, CreateContactRequest, UpdateContactRequest } from './dtos';

@Injectable({ providedIn: 'root' })
export class ContactsApi {
  private http = inject(HttpClient);

  getMy() {
    return this.http.get<ContactDto[]>(`${env.apiBaseUrl}/contacts`);
  }

  create(req: CreateContactRequest) {
    return this.http.post<ContactDto>(`${env.apiBaseUrl}/contacts`, req);
  }

  update(id: string, req: UpdateContactRequest) {
    return this.http.put<ContactDto>(`${env.apiBaseUrl}/contacts/${id}`, req);
  }

  delete(id: string) {
    return this.http.delete<void>(`${env.apiBaseUrl}/contacts/${id}`);
  }
}