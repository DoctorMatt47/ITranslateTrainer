<script lang="ts">
  import { getContext } from "svelte";
  import { goto } from "$app/navigation";
  import AppHeading from "$lib/common/components/AppHeading.svelte";
  import AppTextInput from "$lib/common/components/AppTextInput.svelte";
  import AppNumberInput from "$lib/common/components/AppNumberInput.svelte";
  import AppSubmitButton from "$lib/common/components/AppSubmitButton.svelte";
  import type { TestService } from "$lib/tests/test-service.svelte";

  const testService = getContext<TestService>("testService");

  async function start() {
    console.log(testService.settings);
    await testService.fetchTest();
    await goto("/quiz");
  }
</script>

<AppHeading>Settings</AppHeading>
<form on:submit={start}>
  <div class="w-1/2 mx-auto">
    <div class="grid grid-rows-4 grid-cols-2 gap-4 text-center items-center">
      <div>From:</div>
      <AppTextInput bind:value={testService.settings.fromLanguage} placeholder="from" />
      <div>To:</div>
      <AppTextInput bind:value={testService.settings.toLanguage} placeholder="to" />
      <div>Option count</div>
      <AppNumberInput bind:value={testService.settings.optionCount} />
      <AppSubmitButton className="col-span-2">Start</AppSubmitButton>
    </div>
  </div>
</form>
