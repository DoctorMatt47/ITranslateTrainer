<script lang="ts">
  import type { TranslationApiResponse } from "$lib/translations/translation-api";
  import DeleteTranslationRowButton from "./DeleteTranslationRowButton.svelte";
  import TranslationTextCell from "./TranslationTextCell.svelte";
  import AppTableTd from "$lib/common/components/AppTableTd.svelte";

  const { translation, rowIndex }: {
    translation: TranslationApiResponse;
    rowIndex: number;
  } = $props();
</script>

<tr>
  <AppTableTd>
    {rowIndex + 1}
  </AppTableTd>
  <AppTableTd>
    {translation.originText.language}
  </AppTableTd>
  <AppTableTd>
    {translation.translationText.language}
  </AppTableTd>
  <AppTableTd>
    <TranslationTextCell text={translation.originText} />
  </AppTableTd>
  <AppTableTd>
    <TranslationTextCell text={translation.translationText} />
  </AppTableTd>
  <AppTableTd>
    <DeleteTranslationRowButton translationId={translation.id} />
  </AppTableTd>
</tr>
