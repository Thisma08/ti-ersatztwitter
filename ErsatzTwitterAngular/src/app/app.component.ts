import { Component } from '@angular/core';
import {RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import {ConnectComponent} from './components/user/connect/connect.component';
import {TweetListComponent} from './components/tweet/tweet-list/tweet-list.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ConnectComponent, TweetListComponent, RouterLinkActive, RouterLink],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ErsatzTwitterAngular';
}
