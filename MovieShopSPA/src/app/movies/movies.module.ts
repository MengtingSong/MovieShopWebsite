import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MoviesRoutingModule } from './movies-routing.module';
import { MoviesComponent } from './movies.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { CastDetailsComponent } from './cast-details/cast-details.component';
import { TopRatedComponent } from './top-rated/top-rated.component';


@NgModule({
  declarations: [
    MoviesComponent,
    MovieDetailsComponent,
    CastDetailsComponent,
    TopRatedComponent
  ],
  imports: [
    CommonModule,
    MoviesRoutingModule
  ]
})
export class MoviesModule { }
