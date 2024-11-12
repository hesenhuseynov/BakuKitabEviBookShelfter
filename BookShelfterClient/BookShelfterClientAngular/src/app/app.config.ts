import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ToastrModule } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
     provideRouter(routes),
      provideClientHydration(),
      provideHttpClient(withFetch()),
       provideAnimationsAsync(),
       importProvidersFrom(BrowserAnimationsModule),
       importProvidersFrom(ToastrModule.forRoot({
        timeOut: 2200,
         positionClass: 'toast-top-right', 
         preventDuplicates: true,
         closeButton: true, 
         progressBar: true,  

       })), provideAnimationsAsync()

      // importProvidersFrom(BrowserAnimationsModule),
      // importProvidersFrom(ToastrModule.forRoot())
    ]
};
