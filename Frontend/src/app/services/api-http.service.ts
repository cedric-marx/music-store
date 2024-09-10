import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

export class APIClient {

  protected apiRoot = '';

  constructor(protected http: HttpClient) { }

  public post<T>(method: string, args?: any): Promise<T> {
    return lastValueFrom(this.http.post<T>(this.createMethodUrl(method), args));
  }

  public put<T>(method: string, args: any): Promise<T> {
    return lastValueFrom(this.http.put<T>(this.createMethodUrl(method), args));
  }

  public get<T>(method: string = '', args?: any): Promise<T> {
    return lastValueFrom(this.http.get<T>(this.createMethodUrl(method), { params: args, observe: 'body', responseType: 'json' }));
  }

  public getBlob(method: string, args?: any): Promise<Blob> {
    return lastValueFrom(this.http.get(this.createMethodUrl(method), { params: args, observe: 'body', responseType: 'blob' }));
  }

  public delete<T>(method: string, args?: any): Promise<T> {
    return lastValueFrom(this.http.delete<T>(this.createMethodUrl(method), { params: args, observe: 'body' }));
  }

  public patch<T>(method: string, args: any): Promise<T> {
    return lastValueFrom(this.http.patch<T>(this.createMethodUrl(method), args));
  }

  protected createMethodUrl(method: string): string {
    return method ? (this.apiRoot + '/' + method) : this.apiRoot;
  }
}