import { Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ContactsApi } from '../core/api/contacts.api';
import { ContactDto, CreateContactRequest, UpdateContactRequest } from '../core/api/dtos';

@Injectable({ providedIn: 'root' })
export class ContactsStore {
  private _items = signal<ContactDto[]>([]);
  items = this._items.asReadonly();

  private _loading = signal(false);
  loading = this._loading.asReadonly();


  constructor(private api: ContactsApi) {}

  async loadMy() {
    this._loading.set(true);
    try {
      const data = await firstValueFrom(this.api.getMy());
      this._items.set(data);
    } finally {
      this._loading.set(false);
    }
  }

  async create(req: CreateContactRequest) {
    const created = await firstValueFrom(this.api.create(req));
    this._items.update(list => [created, ...list]);
  }

  async update(id: string, req: UpdateContactRequest) {
    const updated = await firstValueFrom(this.api.update(id, req));
    this._items.update(list => list.map(x => (x.id === id ? updated : x)));
  }

  async delete(id: string) {
    await firstValueFrom(this.api.delete(id));
    this._items.update(list => list.filter(x => x.id !== id));
  }
}