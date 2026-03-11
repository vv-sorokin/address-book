export type AuthResponse = {
  accessToken: string;
  user: { id: string; email: string; role: string };
};

export type ContactDto = {
  id: string;
  firstName: string;
  lastName: string;
  email?: string | null;
  phone?: string | null;
  address?: string | null;
};

export type CreateContactRequest = {
  firstName: string;
  lastName: string;
  email?: string | null;
  phone?: string | null;
  address?: string | null;
};


export type UpdateContactRequest = CreateContactRequest;