import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {Tweet} from '../classes/tweet';

@Injectable({
  providedIn: 'root'
})
export class TweetService {
  private apiUrl = 'http://localhost:5260/api/tweets';

  constructor(private http: HttpClient) {}

  fetchAllTweets(): Observable<Tweet[]> {
    return this.http.get<Tweet[]>(this.apiUrl);
  }

  createTweet(tweet: any): Observable<any> {
    return this.http.post(this.apiUrl, tweet).pipe(
      catchError((error) => {
        if(error.status === 409) {
          console.log(error.error.message);
          return throwError(() => ({message: error.error.message || 'Conflict error.'}));
        }
        return throwError(() => error.error);
      })
    );
  }

  deleteTweet(id: number | undefined): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, {
      withCredentials: true,
    });
  }
}
