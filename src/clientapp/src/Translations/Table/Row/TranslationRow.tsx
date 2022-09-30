import {Translation} from "common/services/translation-service";
import "./TranslationRow.scss";

interface TranslationRowProps {
  translation: Translation;
  index: number;
}

const TranslationRow = ({translation, index}: TranslationRowProps) => {
  return (
    <tr className="translation-row text-center">
      <th scope="row">{index}</th>
      <td className="w-20">{translation.first.language}</td>
      <td className="w-20">{translation.second.language}</td>
      <td className="w-30">{translation.first.string}</td>
      <td className="w-30">{translation.second.string}</td>
    </tr>
  );
};

export default TranslationRow;
