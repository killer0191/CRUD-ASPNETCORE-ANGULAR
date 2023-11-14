import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isLoggedIn = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this.isLoggedIn.asObservable();

  constructor(private router: Router) {
    this.isLoggedIn.next(localStorage.getItem('isLoggedIn') === 'true');
  }

  login() {
    this.isLoggedIn.next(true);
    localStorage.setItem('isLoggedIn', 'true');
  }

  logout() {
    this.isLoggedIn.next(false);
    localStorage.removeItem('isLoggedIn');
    this.router.navigate(['/']);
  }
}
