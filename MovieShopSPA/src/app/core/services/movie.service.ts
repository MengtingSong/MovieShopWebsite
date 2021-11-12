import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Movie } from '../models/movie';
import { MovieCard } from '../models/moviecard'

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  pageTitle = "Movies";
  
  constructor(private http: HttpClient) { }

  // https://localhost:5001/api/Movies/toprevenue
  // many methods that will be used by components
  // HomeComponent will call this function
  getTopRevenueMovies(): Observable<MovieCard[]> {
    // call our API, using HttpClient (XMLHttpRequest) to make GET request
    // HttpClient class comes from HttpClientModule (Angular Team created for us to use)
    // import HttpClientModule inside AppModule
    return this.http.get<MovieCard[]>(`${environment.appBaseUrl}Movies/toprevenue`);
  }
  
  getTopRatedMovies(): Observable<MovieCard[]> {
    return this.http.get<MovieCard[]>(`${environment.appBaseUrl}Movies/toprated`);
  }
  getMovieDetails(id: number): Observable<Movie> {
    return this.http.get<Movie>(`${environment.appBaseUrl}Movies/${id}`)
  }
}
