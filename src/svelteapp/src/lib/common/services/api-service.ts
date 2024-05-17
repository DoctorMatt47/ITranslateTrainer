import { dev } from "$app/environment";
import axios, { type AxiosInstance, type AxiosResponse } from "axios";

export const baseUrl = dev
  ? "http://localhost:5207"
  : "https://api.ourdomain.com";

export type ErrorResponse = {
  errorMessage: string;
}

export class AxiosApiError extends Error {
  response: AxiosResponse;

  constructor(message: string, response: AxiosResponse) {
    super(message);
    this.name = "ApiError";
    this.response = response;
  }
}

export default function createApiInstance(url: string) {
  const instance = axios.create({
    baseURL: `${baseUrl}/api/${url}`,
    timeout: dev ? undefined : 10000,
    headers: {
      "Content-Type": "application/json",
    },
  }) as AxiosInstance;

  instance.interceptors.response.use(throwErrorAxiosInterceptor);

  return instance;
}

function throwErrorAxiosInterceptor(response: AxiosResponse) {
  if (response.status < 200 || response.status >= 300) {
    throw new AxiosApiError(`Request failed with status code ${response.status}`, response);
  }

  return response;
}
