import{ Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import{Subject} from 'rxjs'


@Injectable()

export class ApiService{
  
    private selectedQuestion = new Subject<any>();
    private selectedQuiz = new Subject<any>(); 

    //putting values in components 
    questionSelected = this.selectedQuestion.asObservable();
    quizSelected = this.selectedQuiz.asObservable();

    //DI Concept
    constructor(private http:HttpClient){}
    
    getQuestions(quizId){
        return this.http.get(`http://localhost:20275/api/questions/${quizId}`);
       // .subscribe(res =>{console.log(res)})           
    }

    postQuestion(question){
           this.http.post('http://localhost:20275/api/questions',question).subscribe(res =>
           {console.log(res)})           
    }
   
    getQuizzes(){
        return this.http.get('http://localhost:20275/api/quizzes');
       // .subscribe(res =>{console.log(res)})           
    }
    getAllQuizzes(){
        return this.http.get('http://localhost:20275/api/quizzes/all');
       // .subscribe(res =>{console.log(res)})           
    }
    putQuestion(question){
        this.http.put(`http://localhost:20275/api/questions/${question.id}`,question).subscribe(res =>
        {console.log(res)})           
    }
    
    putQuiz(quiz){
        this.http.put(`http://localhost:20275/api/quizzes/${quiz.id}`,quiz).subscribe(res =>
        {console.log(res)})           
    }
   

    postQuiz(quiz){
        this.http.post('http://localhost:20275/api/quizzes',quiz).subscribe(res =>
        {console.log(res)})           
 }

    //getting selected question 
    selectQuestion(question){
        this.selectedQuestion.next(question);
    }
    
    selectQuiz(quiz){
        this.selectedQuiz.next(quiz);
    }
}