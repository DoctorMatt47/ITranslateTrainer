import {baseUrl} from "../utils/env";
import axios from "axios";
import {TextResponse} from "./translation-service";

const apiTexts = baseUrl + "/api/texts";

export const getTexts = async (): Promise<TextResponse[]> => {
  const response = await axios.get(apiTexts);
  return response.data;
};
