import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Login } from './Interfaces/login';
import { LoginService } from './Services/login.service';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  ListaClientes: Login[] = [];
  formularioCliente: FormGroup;
  formularioTienda: FormGroup;
  isLoggedIn = false;
  showRegisterForm = false;
  registerType: 'cliente' | 'tienda' = 'cliente';
  buttonText = 'Registrarse';
  loginError: string | null = null;
  duplicateEmailMessage: string | null = null;

  constructor(
    private _loginService: LoginService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    //localStorage.removeItem('isLoggedIn');
    this.formularioCliente = fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      nombre: [''],
      apellidos: [''],
      dirección: [''],
    });

    this.formularioTienda = fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      sucursal: [''],
      direccion: [''],
    });
  }
  obtenerClientes() {
    this._loginService.getList().subscribe({
      next: (data) => {
        console.log(data);
        this.ListaClientes = data;
      },
      error: (e) => {
        console.error(e);
      },
    });
  }

  login(email: string, password: string) {
    this._loginService.getLogin(email, password).subscribe({
      next: (cliente) => {
        if (cliente != null) {
          console.log('Inicio de sesión correcto (cliente)');
          console.log('Los datos del cliente son: ', cliente);
          this.authService.login();
          this._loginService
            .getIdClientePorEmail(email)
            .subscribe((idCliente) => {
              console.log('id:', idCliente);
              this.router.navigate(['/home', idCliente]);
            });
        } else {
          this.loginTienda(email, password);
        }
      },
      error: (error) => {
        this.loginTienda(email, password);
      },
    });
  }

  private loginTienda(email: string, password: string) {
    this._loginService.getLoginTienda(email, password).subscribe({
      next: (tienda) => {
        if (tienda != null) {
          console.log('Inicio de sesión correcto (tienda)');
          console.log('Los datos de la tienda son: ', tienda);
          this.authService.login();
          this._loginService
            .getIdTiendaPorEmail(email)
            .subscribe((idTienda) => {
              console.log('id:', idTienda);
              this.router.navigate(['/home-tienda', idTienda]);
            });
        } else {
          console.log('Email y/o contraseña incorrectos');
          this.loginError = 'Email y/o contraseña incorrectos';
        }
      },
      error: (error) => {
        console.error('Email y/o contraseña incorrectos');
        this.loginError = 'Email y/o contraseña incorrectos';
        setTimeout(() => {
          this.loginError = null;
        }, 3000);
      },
    });
  }

  toggleRegisterForm() {
    this.showRegisterForm = !this.showRegisterForm;
    this.formularioCliente.reset();
    this.formularioTienda.reset();
    this.registerType = 'cliente';
  }

  registrar() {
    const formData = this.formularioCliente.value;
    const formData2 = this.formularioTienda.value;

    forkJoin([
      this._loginService.getIdClientePorEmail(formData.email),
      this._loginService.getIdTiendaPorEmail(formData2.email),
    ]).subscribe(([idCliente, idTienda]) => {
      if (idCliente !== null) {
        this.showDuplicateEmailMessage('cliente');
      } else if (idTienda !== null) {
        this.showDuplicateEmailMessage('tienda');
      } else {
        if (this.registerType === 'cliente') {
          this.registerCliente(formData);
        } else if (this.registerType === 'tienda') {
          this.registerTienda(formData2);
        }
      }
    });
  }

  registerCliente(formData: any) {
    console.log('Registrando cliente:', formData);
    console.log('direccion2: ', formData.dirección);
    this._loginService.registrarCliente(formData).subscribe(
      (response) => {
        console.log('Cliente registrado exitosamente:', response);
        window.location.href = '/';
      },
      (error) => {
        console.error('Error al registrar cliente:', error);
      }
    );
  }

  registerTienda(formData: any) {
    console.log('Registrando tienda:', formData);
    console.log('direccion2: ', formData.direccion);
    this._loginService.registrarTienda(formData).subscribe(
      (response) => {
        console.log('Tienda registrada exitosamente:', response);
        window.location.href = '/';
      },
      (error) => {
        console.error('Error al registrar tienda:', error);
      }
    );
  }

  private showDuplicateEmailMessage(type: string) {
    this.duplicateEmailMessage = `Este correo ya está registrado como ${type}.`;
    setTimeout(() => {
      this.duplicateEmailMessage = null;
    }, 3000);
  }
  ngOnInit(): void {
    this.authService.isLoggedIn$.subscribe((isLoggedIn) => {
      this.isLoggedIn = isLoggedIn;
    });
  }

  toggleButtonText() {
    this.showRegisterForm = !this.showRegisterForm;
    this.buttonText = this.showRegisterForm ? 'Iniciar Sesión' : 'Registrarse';
    this.formularioCliente.reset();
    this.formularioTienda.reset();
    this.registerType = 'cliente';
  }
}
