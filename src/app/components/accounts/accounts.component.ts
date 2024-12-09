import { Component, OnInit } from '@angular/core';
import { Account } from './Account';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-accounts',
  standalone: true,
  imports: [],
  templateUrl: './accounts.component.html',
  styleUrl: './accounts.component.css'
})
export class AccountsComponent implements OnInit{
  
  public accounts: Account[] = [];

  constructor( private http: HttpClient)  {}
  ngOnInit(): void {
    this.getAccounts();
  }

  getAccounts() {
    this.http.get<Account[]>(`${environment.baseUrl}/api/Accounts`).subscribe(
      {
        next: result => this.accounts = result,
        error: e => console.error(e)
      }
    );
  }

}
