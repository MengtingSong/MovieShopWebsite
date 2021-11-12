import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-movies',
  template: `
    <p>
      movies works!
    </p>
  `,
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
