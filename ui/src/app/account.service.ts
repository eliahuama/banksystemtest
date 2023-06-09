import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CurrentUserService } from './current-user.service';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "http://localhost:5071";

  constructor(
    private http: HttpClient,
    private currentUser: CurrentUserService
  ) { }

  getAccount(accountId: string) {
    return this.http.get(`${this.baseUrl}/api/account/${accountId}`);
  }

  getAccounts() {
    return this.http.get(`${this.baseUrl}/api/account?userId=${this.currentUser.id}`);
  }

  createAccount(userId: string, initialBalance: number) {
    return this.http.post(`${this.baseUrl}/api/account`, { userId: this.currentUser.id, initialBalance });
  }

  deleteAccount(accountId: string) {
    return this.http.delete(`${this.baseUrl}/api/account/${accountId}`);
  }

  deposit(accountId: string, amount: number) {
    return this.http.post(`${this.baseUrl}/api/account/${accountId}/deposit`, { amount });
  }

  withdraw(accountId: string, amount: number) {
    return this.http.post(`${this.baseUrl}/api/account/${accountId}/withdraw`, { amount });
  }

}
