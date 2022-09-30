import axios from 'axios';
import {baseUrl} from '../utils/env';

export interface Translation {
  id: number;
  first: Text;
  second: Text;
}

export interface Text {
  id: number;
  string: string;
  language: string;
  canBeOption: boolean;
  canBeTested: boolean;
}

const apiTranslations = baseUrl + "/api/translations"

export const getTranslations = async (): Promise<Translation[]> => {
  const response = await axios.get(apiTranslations)
  return response.data;
}
