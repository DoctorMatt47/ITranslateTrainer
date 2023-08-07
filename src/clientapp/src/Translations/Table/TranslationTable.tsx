import { getTranslations, TranslationResponse } from "common/services/translation-service";
import { useEffect, useState } from "react";
import TranslationRow from "./Row/TranslationRow";
import { Table } from "react-bootstrap";

interface TranslationTableProps {
  refresh: boolean;
}

export default function TranslationTable({ refresh }: TranslationTableProps) {
  const [error, setError] = useState<string | null>(null);
  const [translations, setTranslations] = useState<TranslationResponse[] | null>(null);

  useEffect(() => {
    getTranslations()
      .then(t => setTranslations(t))
      .catch(_ => setError("Fetch error"));
  }, [refresh]);

  if (translations === null) {
    return (
      <div className="text-center fs-4">
        {error ?? "Loading..."}
      </div>
    );
  }

  if (translations.length === 0) {
    return (
      <div className="text-center fs-4">
        You don't have any translations. Press "import" to add new
      </div>
    );
  }

  return (
    <div className="table-responsive">
      <Table className="translation-table align-middle">
        <tbody id="translations">
        {translations.map((t, i) =>
          <TranslationRow index={i} translation={t}/>)}
        </tbody>
      </Table>
    </div>
  );
};
