import { api } from "./api-service";
import type { TextResponse } from "./text-service";

export interface PutTranslationRequest {
  firstText: string,
  firstLanguage: string,
  secondText: string,
  secondLanguage: string
}

export interface TranslationResponse {
  id: number;
  first: TextResponse;
  second: TextResponse;
}

const translations = "/translations";

export async function getTranslations(): Promise<TranslationResponse[]> {
  const response = await api.get(translations);
  return response.data;
}

export async function putTranslation(request: PutTranslationRequest): Promise<TranslationResponse> {
  const response = await api.put(translations, request);
  return response.data;
}

export async function importSheet(file: FormData) {
  const response = await api.put(`${translations}/sheet`, file);
  return response.data;
}
