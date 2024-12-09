import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './user';
import { environment } from '../../environments/environment.development';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {
  public users: User[] = [];
  constructor(private http: HttpClient) {} 
  
  ngOnInit(): void {
    this.getUsers();
  }
  getUsers() {
    this.http.get<User[]>(`${environment.baseUrl}/api/Users`).subscribe(
      {
        next: result => this.users = result,
        error: e => console.error(e)
      }
    );
  }

}
