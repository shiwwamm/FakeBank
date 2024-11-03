import { Component } from '@angular/core';
import { UserComponent } from '../user/user.component';
import { AccountsComponent } from '../accounts/accounts.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [UserComponent, AccountsComponent, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  title: string = "FAKE BANK";

}
