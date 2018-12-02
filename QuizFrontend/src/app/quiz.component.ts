import {Component} from '@angular/core'
import {ApiService} from './api.service'

@Component({
selector:'quiz',
templateUrl:'./quiz.component.html'
})

export class QuizComponent{
    
    quiz={};
    constructor(private api:ApiService){}

    //Binds Value to quiz.Componenet.Html
    ngOnInit(){
        this.api.quizSelected.subscribe(quizData => this.quiz = quizData); 
     }
       
}