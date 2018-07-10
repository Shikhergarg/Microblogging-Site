import { Component, OnInit } from '@angular/core';
import { HttpService } from '../services/http-service/http.service';
import { Constants } from '../constant';
import { Router } from '@angular/router';
import { User } from '../users/users.component';
import { CredentialService } from '../services/credentials-service/credentials.service';
@Component({
  selector: 'app-loginregister',
  templateUrl: './loginregister.component.html',
  styleUrls: ['./loginregister.component.css']
})
export class LoginregisterComponent implements OnInit {
  userCredential:UserCredential = new UserCredential()
 
  constructor(private _http: HttpService, private router: Router ,private credentialService :CredentialService) {

  }
 
  ngOnInit() {
  }
 
  loginUser(emailId: string, password: string){
    console.log("Dddd");
    this.userCredential.EmailId = emailId;
    this.userCredential.Password = password;
    console.log("Dddd");
    this._http.LoginUser(Constants.UserConstants.Urls.LoginUser, this.userCredential)
      .subscribe((data : LoginDTO) => {
           if(data.UserDTO == null){
               if(data.ErrorDTOList==null)
                   {console.log("Both null");
                  }
               else
                   {console.log("Error List");}
           }
           else{
             this.credentialService.SaveCredentials(data.UserDTO.EmailId,data.UserDTO.Password,data.UserDTO.UserId);
             this.router.navigate(['/PlayGround']); 
           }
      })
  }
 
  registerUser(){
    this.router.navigate(['/Register']);
  }
 }
 
 export class UserCredential{
  EmailId: string
  Password: string
  UserId:number
 }
 
 export interface LoginDTO {
   ErrorDTOList: string[],
   UserDTO :UserCredential
 }