import { Component, OnInit } from '@angular/core';
import {HttpService} from '../services/http-service/http.service';
import {Constants} from '../constant';
import {CredentialService} from '../services/credentials-service/credentials.service';
import { User } from '../users/users.component';
import {Router} from '@angular/router'; 

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
searchedText: string='';
items: any[]

constructor(private _http: HttpService,private _router: Router,private _credentialService: CredentialService) {
 }
ngOnInit() {
  this.islogin();
this.items = [
{ label: 'People', routerLink: ['users',this.searchedText] },
{ label: 'Posts', routerLink: ['playground',this.searchedText] }
];
}
islogin()
{
  if(this._credentialService.isUserLoggedIn.getValue()==false)
   {
    this._router.navigate(['/loginregister']);
    return;
   }
}
Search(searchText: string) {
console.log(searchText);
this.searchedText = searchText;
this.items[1].routerLink[1] = this.searchedText;
this.items[0].routerLink[1] = this.searchedText;
} 
}
