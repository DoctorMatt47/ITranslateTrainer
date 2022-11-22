import AppButton from "../common/components/AppButton/AppButton";
import {Col, Container, Row} from "react-bootstrap";
import TranslationTable from "./Table/TranslationTable";
import Display from "../common/components/Display/Display";
import {SyntheticEvent} from "react";
import {putSheet} from "../common/services/translation-service";

const Translations = () => {
  return (
    <>
      <Display size={3} text="Translations" className="text-center mb-5"/>

      <Container fluid="lg" className="mb-5">
        <Row>
          <Col className="text-center">
            <AppButton label="Import">
              <input accept=".xlsx"
                     className="form-control"
                     id="import-sheet"
                     type="file"
              onChange={sheetChanged}/>
            </AppButton>
          </Col>
          <Col className="text-center">
            <AppButton label="Export">
              <button/>
            </AppButton>
          </Col>
        </Row>
      </Container>

      <TranslationTable/>

    </>
  );

  async function sheetChanged(event : any) {
      const sheetFile = event.currentTarget.files[0];
      const formData = new FormData();
      formData.append("sheet", sheetFile);
      await putSheet(formData)
  }
};

export default Translations;
