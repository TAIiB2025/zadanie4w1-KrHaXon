import { Component, inject } from '@angular/core';
import { ListaService } from '../lista.service';
import { Observable, of } from 'rxjs';
import { Ksiazka } from '../../models/ksiazka';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-lista',
  imports: [CommonModule, RouterLink],
  templateUrl: './lista.component.html',
  styleUrl: './lista.component.css'
})
export class ListaComponent {
  dane$: Observable<Ksiazka[]> = of([]);
  fraza: string = '';

  constructor(private listaService: ListaService) {}

  ngOnInit(): void {
    this.pobierzWszystkie();
  }

  pobierzWszystkie() {
    this.dane$ = this.listaService.get();
  }

  szukaj() {
    if (!this.fraza || this.fraza.trim() === '') {
      this.pobierzWszystkie();
    } else {
      this.dane$ = this.listaService.getByTytul(this.fraza.trim());
    }
  }
  
}
