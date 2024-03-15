import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Platform',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44377/',
    redirectUri: baseUrl,
    clientId: 'Platform_App',
    responseType: 'code',
    scope: 'offline_access Platform',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44377',
      rootNamespace: 'Promact.CustomerSuccess.Platform',
    },
  },
} as Environment;
