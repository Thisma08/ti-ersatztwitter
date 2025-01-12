import {Component, OnInit} from '@angular/core';
import {TweetService} from '../../../services/tweet.service';
import {Tweet} from '../../../classes/tweet';
import {DeleteTweetComponent} from '../delete-tweet/delete-tweet.component';
import {User} from '../../../classes/user';
import {UserService} from '../../../services/user.service';

@Component({
  selector: 'app-tweet-list',
  imports: [
    DeleteTweetComponent
  ],
  standalone: true,
  templateUrl: './tweet-list.component.html',
  styleUrl: './tweet-list.component.css'
})
export class TweetListComponent implements OnInit {
  tweets: Tweet[] = [];
  users: User[] = [];

  constructor(private tweetService: TweetService, private userService: UserService) {
  }

  ngOnInit(): void {
    this.loadTweets();
    this.loadUsers();
  }

  private loadTweets() {
    this.tweetService.fetchAllTweets().subscribe((data: Tweet[]) => {
      this.tweets = data;
    });
  }

  private loadUsers() {
    this.userService.fetchAllUsers().subscribe((data: User[]) => {
      this.users = data;
    });
  }

  onTweetDeleted(tweetToDelete: Tweet): void {
    this.tweets = this.tweets.filter(music => music.id !== tweetToDelete.id);
  }

  getUserPseudo(idUser: number): string {
    const user = this.users.find(u => u.id === idUser);
    return user ? user.pseudo : 'Unknown';
  }
}
