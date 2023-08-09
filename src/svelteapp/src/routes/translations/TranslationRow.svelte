<script lang="ts">
  import type { TranslationResponse } from "$lib/services/translation-service";

  export let translation: TranslationResponse;
  export let rowIndex: number;

  function rowValues(translation: TranslationResponse, rowIndex: number) {
    return [
      rowIndex + 1,
      translation.first.language,
      translation.second.language,
      translation.first.string,
      translation.second.string,
    ];
  }

</script>

<tr>
  {#each rowValues(translation, rowIndex) as value}
    <td class="text-center">{value}</td>
  {/each}
  <td class="text-center">
    <i class="fa fa-trash"></i>
  </td>
</tr>
