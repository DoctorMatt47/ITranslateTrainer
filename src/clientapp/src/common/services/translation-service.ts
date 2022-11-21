import axios from 'axios';
import {baseUrl} from 'common/utils/env';

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
}

const apiTranslations = baseUrl + "/api/translations";

export const getTranslations = async (): Promise<TranslationResponse[]> => {
  const response = await axios.get(apiTranslations);
  return response.data;
};
