import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {FormsModule} from '@angular/forms';
import { BookentryComponent } from "./bookentry/bookentry.component";
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgIf, NgFor, FormsModule, BookentryComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'library';
  name = 'Neel';
  // bookname = '';
  // books: string[] = [];
  // addItem(){
  //   if(this.bookname.trim() !== '') {
  //     this.books.push(this.bookname);
  //     this.bookname = '';
  //   }
  // }
  // deleteItem(book: string){
    
  //   this.books = this.books.filter( i => i !== book);
  // }
  // updateItem(book: string){
  //   this.deleteItem(book);
  //   this.bookname = book;
  // }
  }

