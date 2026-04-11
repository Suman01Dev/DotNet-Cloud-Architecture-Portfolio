import { Component, OnInit, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

interface ContainerData {
  id: string;
  name: string;
  image: string;
  status: string;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'] // Notice 'styleUrls' plural
})
export class AppComponent implements OnInit {
  private http = inject(HttpClient);
  
  containers = signal<ContainerData[]>([]);
  errorMessage = signal<string>('');

  ngOnInit() {
    this.http.get<ContainerData[]>('http://localhost:5000/api/containers')
      .subscribe({
        next: (data) => this.containers.set(data),
        error: (err) => this.errorMessage.set('Failed to connect to the .NET API.')
      });
  }
}