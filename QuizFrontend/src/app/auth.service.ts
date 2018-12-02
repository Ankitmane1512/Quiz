import{ Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';


@Injectable()

export class AuthService{
  
    //DI Concept
    constructor(private http:HttpClient, private router:Router){}
        
    get isAuthenticated(){
        //!! return true if exist and ! returns treu if doesnt exist.
        return !!localStorage.getItem('token');
    } 

    register(credentials){
           this.http.post<any>('http://localhost:20275/api/account',credentials).subscribe(res=>{
            this.authenticate(res)
           });   
    }
    login(credentials){
        this.http.post<any>('http://localhost:20275/api/account/login',credentials).subscribe(res=>{
            this.authenticate(res)
        });
    }

    logout (){
        localStorage.removeItem('token');
    }
        
        authenticate(res){
            localStorage.setItem('token', res);
            this.router.navigate(['/']);
        }

   
    
}