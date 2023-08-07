import { api } from "./api-service";

export interface TranslationResponse {
  id: number;
  first: TextResponse;
  second: TextResponse;
}

export interface TextResponse {
  id: number;
  string: string;
  language: string;
  canBeOption: boolean;
  canBeTested: boolean;
  correctCount: number;
  incorrectCount: number;
}

const translations = "/translations";

export async function getTranslations(): Promise<TranslationResponse[]> {
  const response = await api.get(translations);
  return response.data;
}

export async function importSheet(file: FormData) {
  const response = await api.put(`${translations}/sheet`, file);
  return response.data;
}
