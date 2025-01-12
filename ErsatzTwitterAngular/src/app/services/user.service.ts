import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {User} from '../classes/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5260/api/users';

  constructor(private http: HttpClient) {}

  fetchAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  connectUser(userId: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/connect/${userId}`, null, {
      withCredentials: true,
    });
  }
}
