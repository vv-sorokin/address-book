import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

@Component({
  standalone: true,
  selector: 'app-landing-page',
  imports: [RouterLink, MatToolbarModule, MatButtonModule, MatCardModule],
  templateUrl: './landing.page.html',
  styleUrl: './landing.page.scss',
})
export class LandingPage {}