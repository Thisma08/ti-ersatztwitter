import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {Tweet} from '../classes/tweet';

@Injectable({
  providedIn: 'root'
})
export class LikeService {
  private apiUrl = 'http://localhost:5260/api/likes';

  constructor(private http: HttpClient) {}

  createLike(userId: number | null, tweetId: number | undefined): Observable<any> {
    return this.http.post(this.apiUrl, {userId, tweetId}).pipe(
      catchError((error) => {
        if(error.status === 409) {
          console.log(error.error.message);
          return throwError(() => ({message: error.error.message || 'Conflict error.'}));
        }
        return throwError(() => error.error);
      })
    );
  }
}
