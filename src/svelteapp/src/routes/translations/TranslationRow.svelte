<script lang="ts">
  import type { TranslationApiResponse } from "$lib/translations/translation-api";
  import DeleteTranslationRowButton from "./DeleteTranslationRowButton.svelte";
  import TextCell from "./TextCell.svelte";

  const { translation, rowIndex }: {
    translation: TranslationApiResponse;
    rowIndex: number;
  } = $props();
</script>

<tr>
  <td class="text-center">
    {rowIndex + 1}
  </td>
  <td class="text-center">
    {translation.originText.language}
  </td>
  <td class="text-center">
    {translation.translationText.language}
  </td>
  <td class="text-center">
    <TextCell text={translation.originText} />
  </td>
  <td class="text-center">
    <TextCell text={translation.translationText} />
  </td>
  <td class="text-center">
    <DeleteTranslationRowButton translationId={translation.id} />
  </td>
</tr>
