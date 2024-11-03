import { Routes } from '@angular/router';
import { UserComponent } from './components/user/user.component';
import { AccountsComponent } from './components/accounts/accounts.component';

export const routes: Routes = [
	{
		path: "users",
		component: UserComponent
	},
	{
		path: "accounts",
		component: AccountsComponent
	}
];
