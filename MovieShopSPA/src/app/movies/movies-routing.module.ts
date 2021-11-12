import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CastDetailsComponent } from './cast-details/cast-details.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { MoviesComponent } from './movies.component';
import { TopRatedComponent } from './top-rated/top-rated.component';

const routes: Routes = [
  { path: "", component: MoviesComponent, 
      children: [
        { path: ":id", component: MovieDetailsComponent },
        { path: "cast/:id", component:CastDetailsComponent },
        { path: "toprated", component: TopRatedComponent }
      ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MoviesRoutingModule { }
