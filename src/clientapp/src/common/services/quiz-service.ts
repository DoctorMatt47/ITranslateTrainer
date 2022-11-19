import axios from 'axios';
import {baseUrl} from 'common/utils/env';

interface QuizOptionData {
  string: string;
  isCorrect: boolean;
}

export interface QuizTestData {
  string: string,
  options: QuizOptionData[]
}

const apiQuiz = baseUrl + "/api/quiz";

interface GetQuizParams {
  from: string;
  to: string;
  testCount: number;
  optionCount: number;
}

export const getQuiz =
  async (params: GetQuizParams): Promise<QuizTestData[]> => {
    const response = await axios.get(apiQuiz, {params});
    return response.data;
  };
