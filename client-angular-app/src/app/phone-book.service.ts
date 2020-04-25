
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { PhoneBook } from './model/phonebook';

@Injectable({
  providedIn: 'root'
})
export class PhoneBookService {

  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'phonebook/';
  }

  getPhoneBooks(): Observable<PhoneBook[]> {
    return this.http.get<PhoneBook[]>(this.myAppUrl + this.myApiUrl)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getPhoneBook(id: string): Observable<PhoneBook> {
    return this.http.get<PhoneBook>(this.myAppUrl + this.myApiUrl + id)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
}

savePhoneBook(PhoneBook): Observable<PhoneBook> {
    return this.http.post<PhoneBook>(this.myAppUrl + this.myApiUrl, JSON.stringify(PhoneBook), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
}

updatePhoneBook(postId: string, PhoneBook): Observable<PhoneBook> {

    console.log(this.myAppUrl + this.myApiUrl + postId);
    return this.http.put<PhoneBook>(this.myAppUrl + this.myApiUrl + postId, JSON.stringify(PhoneBook), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
}

deletePhoneBook(postId: string): Observable<PhoneBook> {
    return this.http.delete<PhoneBook>(this.myAppUrl + this.myApiUrl + postId)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
}

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
