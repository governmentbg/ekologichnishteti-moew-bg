import { HttpClient, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Configuration } from "../configuration/configuration";
import { Observable, map } from "rxjs";
import { ZopoeshtAttachedFile } from "../models/zopoesht-attached-file";

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {
  constructor(
    private http: HttpClient,
    private configuration: Configuration) { }

  uploadFile(file: File, fileStorageUrl: string): Observable<HttpEvent<ZopoeshtAttachedFile>> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<ZopoeshtAttachedFile>(fileStorageUrl, formData,
      { reportProgress: true, observe: 'events' });
  }

  getFile(fileUrl: string, mimeType: string): Observable<Blob> {
    return this.http.get(fileUrl,
      { responseType: 'blob' })
      .pipe(
        map(response => new Blob([response], { type: mimeType }))
      );
  }

  getFileToAnchorUrl(file: ZopoeshtAttachedFile) {
    const fileUrl = `${this.configuration.restUrl}/FilesStorage?key=${file.key}&fileName=${file.name}&dbId=${file.dbId}`;

    return this.getFile(fileUrl, file.mimeType)
      .subscribe((blob: Blob) => {
        var url = window.URL.createObjectURL(blob);
        window.open(url);
      });
  }
}