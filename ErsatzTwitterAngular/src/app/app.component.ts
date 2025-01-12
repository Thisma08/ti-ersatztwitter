import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {ConnectComponent} from './components/user/connect/connect.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ConnectComponent],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ErsatzTwitterAngular';
}
