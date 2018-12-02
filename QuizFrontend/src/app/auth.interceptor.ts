import{ Injectable} from '@angular/core';
import {HttpInterceptor} from '@angular/common/http';


@Injectable()

export class AuthInterceptor implements HttpInterceptor{
  
    //DI Concept
    constructor(){}
        
    intercept(req, next)
    {
        //to Check Whther interceptor is working or not
        //console.log(req);
        //return next.handle(req);
        var token = localStorage.getItem('token');
        var authRequest = req.clone({
            headers:req.headers.set('Authorization',`Bearer ${token}`)
        });
        return next.handle(authRequest);
    }
       
    
}